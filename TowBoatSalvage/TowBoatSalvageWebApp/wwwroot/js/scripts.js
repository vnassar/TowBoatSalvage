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
