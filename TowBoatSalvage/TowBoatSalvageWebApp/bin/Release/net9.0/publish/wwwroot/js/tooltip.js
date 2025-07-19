export function initializeTooltips() {
    document.body.addEventListener("mouseenter", handleMouseEnter, true);
    document.body.addEventListener("mouseleave", handleMouseLeave, true);
}

export function disposeTooltips() {
    document.body.removeEventListener("mouseenter", handleMouseEnter, true);
    document.body.removeEventListener("mouseleave", handleMouseLeave, true);
}

function handleMouseEnter(e) {
    const container = e.target.closest('.input-tooltip-container');
    if (container && (e.target.tagName === 'INPUT' || e.target.tagName === 'SELECT')) {
        const tooltip = container.querySelector('.input-tooltip');
        if (tooltip) tooltip.classList.add('active');
    }

    if (e.target.classList.contains('input-tooltip') ||
        e.target.closest('.input-tooltip')) {
        const tooltip = e.target.classList.contains('input-tooltip') ?
            e.target : e.target.closest('.input-tooltip');
        tooltip.classList.add('active');
    }
}

function handleMouseLeave(e) {
    const container = e.target.closest('.input-tooltip-container');
    if (container && (e.target.tagName === 'INPUT' || e.target.tagName === 'SELECT')) {
        if (!e.relatedTarget || !e.relatedTarget.closest('.input-tooltip')) {
            const tooltip = container.querySelector('.input-tooltip');
            if (tooltip) tooltip.classList.remove('active');
        }
    }

    if (e.target.classList.contains('input-tooltip')) {
        if (!e.relatedTarget || !e.relatedTarget.closest('.input-tooltip-container')) {
            e.target.classList.remove('active');
        }
    }
}