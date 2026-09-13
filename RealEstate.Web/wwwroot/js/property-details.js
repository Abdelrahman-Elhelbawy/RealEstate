/* Property Details Gallery & Contact Form Helpers */
document.addEventListener('DOMContentLoaded', function () {
    const mainImg = document.getElementById('main-gallery-img');
    const thumbs = document.querySelectorAll('.gallery-thumb-item');

    thumbs.forEach(thumb => {
        thumb.addEventListener('click', function () {
            thumbs.forEach(t => t.classList.remove('active'));
            this.classList.add('active');

            const newSrc = this.getAttribute('data-img-src');
            if (mainImg && newSrc) {
                mainImg.src = newSrc;
            }
        });
    });

    // Copy link helper
    const shareBtn = document.getElementById('btn-share-property');
    if (shareBtn) {
        shareBtn.addEventListener('click', function () {
            navigator.clipboard.writeText(window.location.href).then(() => {
                showToast('Property link copied to clipboard!', 'info');
            }).catch(() => {
                showToast('Could not copy link', 'error');
            });
        });
    }
});
