(() => {
    class Cart {
        getItems() {
            const json = window.localStorage.getItem(Cart.key);

            return json ? JSON.parse(json) : Cart.initialValue;
        }

        add(item) {
            const items = this.getItems();

            if (item.id in items) {
                items[item.id] += 1;
            } else {
                items[item.id] = 1;
            }

            this.__setItems(items);
        }

        remove(item) {
            const items = this.getItems();

            delete items[item.id];
            this.__setItems(items);
        }

        clear() {
            this.__setItems(Cart.initialValue);
        }

        __setItems(items) {
            window.localStorage.setItem(Cart.key, JSON.stringify(items));
        }
    }

    Cart.key = 'CART_ITEMS_KEY';
    Cart.initialValue = {};

    window.cart = new Cart();
})();