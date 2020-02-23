(() => {
    document.addEventListener('DOMContentLoaded', e => {
        const notification = getNotificationMessage();
        notify(notification);
    });

    function getNotificationMessage() {
        const script = document.getElementById('notification');
        const data = script.dataset['message'];
        if (data) {
            return JSON.parse(data);
        }

        return null;
    }

    function notify(notification) {
        if (!notification) {
            return;
        }

        switch (notification.Type) {
            case "Success":
                toastr.success(notification.Message);
                break;
            case "Error":
                toastr.error(notification.Message);
                break;
            case "Info":
                toastr.info(notification.Message);
                break;
            default:
                break;
        }
    }
})();