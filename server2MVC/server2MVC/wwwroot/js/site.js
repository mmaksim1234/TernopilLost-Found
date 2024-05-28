document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('announcementModal');
    const closeBtn = modal.querySelector('.close');

    document.querySelectorAll('.details-button').forEach(function (button) {
        button.addEventListener('click', function () {
            const announcement = this.closest('.announcement');
            const category = announcement.querySelector('.category-label').innerText;
            const imageSrc = announcement.querySelector('.announcementImg img').src;
            const title = announcement.querySelector('.title h3').innerText;
            const description = announcement.querySelector('.text p').innerText;
            const phone = announcement.querySelector('.phone p').innerText;

            modal.querySelector('.modal-category p').innerText = category;
            modal.querySelector('.modal-image img').src = imageSrc;
            modal.querySelector('.modal-title h3').innerText = title;
            modal.querySelector('.modal-description p').innerText = description;
            modal.querySelector('.modal-phone p').innerText = phone;

            modal.style.display = "block";
        });
    });

    closeBtn.addEventListener('click', function () {
        modal.style.display = "none";
    });

    window.addEventListener('click', function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    });
});