using System.Text.Json;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.DAL
{
    public class MessageManager
    {
        //private static Uri BaseAddress = new("https://localhost:44392/");
        private static Uri BaseAddress = new("https://messagesapi.azurewebsites.net/");

        public static async Task<List<Message>> GetAllMessages()
        {
            List<Message> messages = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("api/Messages");
                //Get i apiet körs

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    messages = JsonSerializer.Deserialize<List<Message>>(responseString);
                }
            }

            return messages;
        }

        public static async Task<Message> GetMessage(int id)
        {
            Message message = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("api/Messages/" + id);
                //Get med id i APIet körs

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    message = JsonSerializer.Deserialize<Message>(responseString);
                }
            }

            return message;
        }


        public static async Task DeleteMessage(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.DeleteAsync("api/Messages/" + id);
                //Delete i apiet körs
            }
        }


  

        public static async Task SaveMessage(Message message)
        {
            Message mess = (await GetAllMessages()).FirstOrDefault(m => m.Id == message.Id);

            if (mess != null) //Uppdatera
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = BaseAddress;
                    var json = JsonSerializer.Serialize(message);
                    StringContent httpContent = new(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("api/Messages/" + mess.Id, httpContent);
                    //Put med id, put i APIet körs
                }
            }
            else
            {
                using (var client = new HttpClient()) //Skapa ny
                {
                    client.BaseAddress = BaseAddress;
                    var json = JsonSerializer.Serialize(message);
                    StringContent httpContent = new(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Messages", httpContent);
                    //post utan Id, Post i apiet körs

                }
            }
        }

    }
}
