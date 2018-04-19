$(document).ready(function () {
    $('#rootwizard').bootstrapWizard({
        'tabClass': 'nav nav-pills',
        'withVisible': false,
        'onNext': function (tab, navigation, index) {
        },
        'onTabClick': function (tab, navigation, index) {
            return false;
        }
    });
 });