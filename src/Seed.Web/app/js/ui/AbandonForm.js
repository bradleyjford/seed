(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('abandonForm', ['$state', 'confirm', function ($state, confirm) {
        return {
            restrict: 'A',
            require: 'form',
            link: function (scope, element, attrs, formController) {
                var isWarning = false;

                scope.$on('$stateChangeStart', function (event, toState, toParams) {
                    if (!isWarning && formController.$dirty) {
                        isWarning = true;

                        event.preventDefault();

                        confirm.show(
                            scope,
                            'Abandon changes?',
                            'Are you sure that you abandon your changes?',
                            {
                                okButtonText: 'Yes, continue',
                                okButtonClass: 'btn-warning',
                                cancelButtonText: 'No, cancel'
                            })
                            .then(function () {
                                $state.go(toState, toParams);
                                isWarning = false;
                            }, function () {
                                isWarning = false;
                            });
                    }
                });
            }
        };
    }]);
})(angular);