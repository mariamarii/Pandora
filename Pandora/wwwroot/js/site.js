
    var swiper = new Swiper('.swiper-container', {
        loop: true,
    autoplay: {
        delay: 5000, // Change slide every 3 seconds
    disableOnInteraction: false, // Continue autoplay after user interactions
            },
    pagination: {
        el: '.swiper-pagination',
    clickable: true,
            },
    navigation: {
        nextEl: '.swiper-button-next',
    prevEl: '.swiper-button-prev',
            },
        effect:'fade' // Use fade effect for smooth transitions
        });
