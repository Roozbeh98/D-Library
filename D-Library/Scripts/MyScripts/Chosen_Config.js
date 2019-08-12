var config = {
    '.chosen-select': { no_results_text: 'موردی یافت نشده !', disable_search_threshold: 5, rtl: true },
    '.chosen-select-deselect': { allow_single_deselect: true },
    '.chosen-select-no-single': { disable_search_threshold: 10 },
    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },

    '.chosen-select-width': { width: '100vh' }
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}
