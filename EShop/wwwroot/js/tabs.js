(() => {
    document.addEventListener('DOMContentLoaded', e => {
        const tab = getActiveTab();
        if (tab) {
            $(`#${tab}-tab`).tab('show');
        }
    });

    function getActiveTab() {
        const element = document.getElementById('tab-container');
        if (!element) {
            return null;
        }

        const activeTab = element.dataset['activeTab'];
        if (!activeTab) {
            return null;
        }

        switch (activeTab) {
            case "Storage":
                return "storage";
            case "Catalog":
                return "catalog";
            default:
                return null;
        }
    }
})();