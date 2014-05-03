(function (angular) {
    'use strict';

    var saFocusDirective = function () {
        return function (scope, elem, attr) {
            scope.$on('$viewContentLoaded', function (event) {
                elem.focus();
            });
        };
    };

    angular.module('seedApp')
        .directive('seFocus', saFocusDirective);

})(angular);