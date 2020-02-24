(() => {
    document.addEventListener('DOMContentLoaded', e => {
        const cart = window.cart;
        if (!cart) {
            return;
        }

        initializeCartAdd(cart, true);
        initializeCartRemove(cart, true);
        initializeClearCart(cart);
    });

    function initializeCartAdd(cart, withListeners) {
        const links = document.querySelectorAll('button[data-cart="add"]');

        for (let link of links) {
            const id = parseInt(link.dataset['id']);
            const max = parseInt(link.dataset['max']);

            const items = cart.getItems();
            if (id in items) {
                link.innerText = `Add to cart (${items[id]})`;

                if (items[id] === max) {
                    link.disabled = true;
                }
            } else {
                link.disabled = false;
                link.innerText = 'Add to cart';
            }

            if (withListeners) {
                link.addEventListener('click', _ => {
                    cart.add({ id });

                    const items = cart.getItems();

                    link.innerText = `Add to cart (${items[id]})`;

                    if (items[id] === max) {
                        link.disabled = true;
                    }

                    initializeCartRemove(cart, false);
                });
            }
        }
    }

    function initializeCartRemove(cart, withListeners) {
        const links = document.querySelectorAll('button[data-cart="remove"]');

        for (let link of links) {
            const id = parseInt(link.dataset['id']);

            const items = cart.getItems();
            link.disabled = !(id in items);

            if (withListeners) {
                link.addEventListener('click', _ => {
                    cart.remove({ id });
                    link.disabled = true;

                    initializeCartAdd(cart, false);
                });
            }
        }
    }

    function initializeClearCart(cart) {
        const clearCart = document.getElementById('clear-cart');

        if (!clearCart) { return; }

        clearCart.addEventListener('click', () => {
            cart.clear();

            initializeCartAdd(cart, false);
            initializeCartRemove(cart, false);
        })
    }
})();