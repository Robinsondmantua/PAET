(function ($) {
    function initSecondaryCodeEditor() {
        var $active = $('#preguntas_test > .active > a');
        var $sec_tab = $($active.data('target'));
        // --> 1. & 2. & 3.: try to find an already existing CodeMirror instance (https://github.com/codemirror/CodeMirror/issues/1413)
        // if found, simply refresh it!
        var codeMirrorContainer = $sec_tab.find(".CodeMirror")[0];
        if (codeMirrorContainer && codeMirrorContainer.CodeMirror) {
            codeMirrorContainer.CodeMirror.refresh();
        } else {
            //CodeMirror.fromTextArea($sec_tab.find('textarea')[0], {
            //    lineNumbers: true
            //});
        }
        // <--
    }
$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({
        'tabClass': 'nav nav-pills',
        'withVisible': false
    });

    // https://codepen.io/lloiser/pen/arBkv Este código permite buscar las instancias
    // del editor de código CodeMirror y refrescarlas en las pestañas que están ocultas.
    $('#preguntas_test > li > a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            if ($(e.target).is(":visible")) {
                initSecondaryCodeEditor();
            }
            // <--
        });
        var json, tabsState;
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            tabsState = localStorage.getItem("tabs-state");
            json = JSON.parse(tabsState || "{}");
            json[$(e.target).parents("ul.nav.nav-pills, ul.nav.nav-tabs").attr("id")] = $(e.target).data('target');

            localStorage.setItem("tabs-state", JSON.stringify(json));
        });

        tabsState = localStorage.getItem("tabs-state");

        json = JSON.parse(tabsState || "{}");
        $.each(json, function (containerId, target) {
            return $("#" + containerId + " a[data-target=" + target + "]").tab('show');
        });

        $("ul.nav.nav-pills, ul.nav.nav-tabs").each(function () {
            var $this = $(this);
            if (!json[$this.attr("id")]) {
                return $this.find("a[data-toggle=tab]:first, a[data-toggle=pill]:first").tab("show");
            }
        });

    });
})(jQuery);