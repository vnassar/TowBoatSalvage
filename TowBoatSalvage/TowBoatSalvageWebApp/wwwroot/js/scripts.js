window.registerClickOutside = (dotNetHelper, menuId) => {
    const handler = (event) => {
        const menu = document.getElementById(menuId);
        if (menu && !menu.contains(event.target)) {
            dotNetHelper.invokeMethodAsync("CloseColumnMenu");
            document.removeEventListener("mousedown", handler);
        }
    };
    document.addEventListener("mousedown", handler);
};

window.registerClickOutsideRow = function (dotNetHelper, elementId) {
    function handler(event) {
        const menu = document.getElementById(elementId);
        if (menu && !menu.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('CloseRowMenu');
            document.removeEventListener('mousedown', handler);
        }
    }
    setTimeout(() => { // Allow the menu to open before listening
        document.addEventListener('mousedown', handler);
    }, 100);
};
