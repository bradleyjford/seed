(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saSpinner', ['$rootScope', function ($rootScope) {
        return {
            restrict: 'EA',
            templateUrl: 'common/Spinner.html',
            scope: { },
            link: function (scope) {
                scope.enabled = false;

                scope.$on('$stateChangeStart', function () {
                    scope.enabled = true;
                });

                scope.$on('$stateChangeSuccess', function () {
                    scope.enabled = false;
                });

                scope.$on('$stateChangeError', function () {
                    scope.enabled = false;
                });
            }
        };
    }]);
})(angular);
