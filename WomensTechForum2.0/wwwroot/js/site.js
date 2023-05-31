var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})



function openForm() {
    document.getElementById("myForm").style.display = "block";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
}

function openPTForm(postThreadId) {
    var form = document.querySelector(".myPTForm-" + postThreadId);
    form.style.display = "block";
}

function closePTForm(postThreadId) {
    var form = document.querySelector(".myPTForm-" + postThreadId);
    form.style.display = "none";
}


var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

//$(document).ready(function () {
//    $('.clickable-row').click()
//})



//RoleAdminModal
const myModal = new bootstrap.Modal(document.getElementById('myModal'), {});
const modalTriggerButtons = document.querySelectorAll('.modal-trigger');
const confirmDeleteButton = document.getElementById('confirmDelete');

modalTriggerButtons.forEach(button => {
    button.addEventListener('click', function () {
        const roleName = this.getAttribute('data-modal-role-name');
        const userId = this.getAttribute('data-modal-user-id');

        confirmDeleteButton.href = `?RemoveUserId=${userId}&Role=${roleName}`;
        // Öppnar modalen
        myModal.show();
    });
});




//PostThreadModal
const myPTModal = new bootstrap.Modal(document.getElementById('myPTModal'), {});
/*const modalTriggerButton = document.getElementById('modal-trigger');*/
const modalTriggerPTButtons = document.querySelectorAll('.modal-PT-trigger');

const confirmReportLink = document.getElementById('confirmReport');

modalTriggerPTButtons.forEach(button => {
    button.addEventListener('click', function () {
        const postthreadId = this.getAttribute('data-modal-postthread-id');

        confirmReportLink.href = `?changePTId=${postthreadId}`;

        myPTModal.show();
    });
});


//// Funktion för att gilla ett inlägg
//function likePostThread(event) {
//    event.preventDefault();

//    // Hämta PT-id från data-attributet
//    const ptId = event.target.getAttribute('data-ptid');

//    // Skapa en AJAX-förfrågan
//    const xhr = new XMLHttpRequest();
//    xhr.open('GET', '/Controller/LikePostThread?id=' + ptId, true);
//    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

//    xhr.onload = function () {
//        if (xhr.status === 200) {
//            const response = JSON.parse(xhr.responseText);
//            if (response.success) {
//                // Gilla-åtgärden lyckades, uppdatera gränssnittet om det behövs
//                event.target.innerHTML = '<i class="bi bi-heart-fill text-danger mx-1"></i>';
//                event.target.setAttribute('onclick', 'unlikePostThread(event)');
//                event.target.classList.remove('like-button');
//                event.target.classList.add('unlike-button');
//            } else {
//                // Ett fel uppstod, hantera felmeddelandet
//                console.error(response.message);
//            }
//        }
//    };

//    // Skicka förfrågan
//    xhr.send();
//}

//// Funktion för att ogilla ett inlägg
//function unlikePostThread(event) {
//    event.preventDefault();

//    // Hämta PT-id från data-attributet
//    const ptId = event.target.getAttribute('data-ptid');

//    // Skapa en AJAX-förfrågan
//    const xhr = new XMLHttpRequest();
//    xhr.open('GET', '/Controller/UnlikePostThread?id=' + ptId, true);
//    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

//    xhr.onload = function () {
//        if (xhr.status === 200) {
//            const response = JSON.parse(xhr.responseText);
//            if (response.success) {
//                // Ogilla-åtgärden lyckades, uppdatera gränssnittet om det behövs
//                event.target.innerHTML = '<i class="bi bi-heart text-danger mx-1"></i>';
//                event.target.setAttribute('onclick', 'likePostThread(event)');
//                event.target.classList.remove('unlike-button');
//                event.target.classList.add('like-button');
//            } else {
//                // Ett fel uppstod, hantera felmeddelandet
//                console.error(response.message);
//            }
//        }
//    };

//    // Skicka förfrågan
//    xhr.send();
//}