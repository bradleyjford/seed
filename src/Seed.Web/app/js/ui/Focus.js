(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saFocus', [function () {
        return function (scope, elem, attrs) {
            scope.$on('$viewContentLoaded', function () {
                elem.focus();
            });
        };
    }]);

})(angular);
