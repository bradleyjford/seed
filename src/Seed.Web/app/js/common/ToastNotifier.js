(function (angular, toastr) {
    'use strict';

    angular.module('seedApp')
        .factory('ToastNotifier', [function () {
            return {
                info: function (title, message) {
                    toastr.info(message, title);
                },
                success: function (title, message) {
                    toastr.success(message, title);
                },
                warn: function (title, message) {
                    toastr.warning(message, title);
                },
                error: function (title, message) {
                    toastr.error(message, title);
                }
            };
        }]);
})(angular, toastr);