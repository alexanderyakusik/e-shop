(() => {
    document.addEventListener('DOMContentLoaded', e => {
        const cart = window.cart;
        if (!cart) {
            return;
        }

        const link = document.getElementById('order');
        if (!link) {
            return;
        }

        link.addEventListener('click', e => {
            cart.clear();
        });
    });
})();