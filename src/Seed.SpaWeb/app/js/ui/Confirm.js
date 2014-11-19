(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.provider('confirm', function () {
        var confirmProvider = {
            options: {
                okButtonText: 'OK',
                okButtonClass: 'btn-primary',
                cancelButtonText: 'Cancel',
                cancelButtonClass: ''
            },

            $get: ['$modal', '$interpolate', function ($modal, $interpolate) {
                var confirm = { };

                confirm.show = function (scope, title, body, confirmOptions) {
                    confirmOptions = angular.extend({}, confirmProvider.options, confirmOptions);

                    var modalInstance = $modal.open({
                        backdrop: 'static',
                        templateUrl: 'ui/Confirm.html',
                        resolve: {
                            data : function () {
                                return {
                                    title: $interpolate(title)(scope),
                                    body: $interpolate(body)(scope)
                                };
                            },
                            opts : function () {
                                return confirmOptions;
                            }
                        },
                        controller: [
                            '$scope', '$modalInstance', 'data', 'opts',
                            function ($scope, $modalInstance, data, opts) {
                                $scope.data = data;
                                $scope.opts = opts;

                                $scope.ok = function () {
                                    $modalInstance.close(true);
                                };

                                $scope.cancel = function () {
                                    $modalInstance.dismiss(false);
                                };
                            }
                        ]
                    });

                    return modalInstance.result;
                };

                return confirm;
            }]
        };

        return confirmProvider;
    });
})(angular);