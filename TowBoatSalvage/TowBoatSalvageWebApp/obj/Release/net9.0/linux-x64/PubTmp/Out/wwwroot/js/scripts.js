window.registerClickOutside = (dotNetHelper, menuId) => {
    const handler = (event) => {
        const menu = document.getElementById(menuId);
        if (menu && !menu.contains(event.target)) {
            dotNetHelper.invokeMethodAsync("CloseColumnMenu");
            document.removeEventListener("mousedown", handler);
        }
    };
    document.addEventListener("mouseup", handler);
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

window.getBoundingClientRect = function (element) {
    if (!element) return null;
    const rect = element.getBoundingClientRect();
    return {
        left: rect.left,
        top: rect.top,
        bottom: rect.bottom,
        right: rect.right,
        width: rect.width,
        height: rect.height
    };
};

window.registerMenuReposition = function (dotNetRef, element) {
    function reposition() {
        if (!element) return;
        const rect = element.getBoundingClientRect();
        dotNetRef.invokeMethodAsync('UpdateMenuPosition', {
            left: rect.left,
            top: rect.bottom
        });
    }
    window._menuRepositionHandler = reposition;
    window.addEventListener('scroll', reposition, true);
    window.addEventListener('resize', reposition, true);
};

window.unregisterMenuReposition = function () {
    if (window._menuRepositionHandler) {
        window.removeEventListener('scroll', window._menuRepositionHandler, true);
        window.removeEventListener('resize', window._menuRepositionHandler, true);
        window._menuRepositionHandler = null;
    }
};
