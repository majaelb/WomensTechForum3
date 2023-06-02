

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