(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saNavActive', ['$state', function ($state) {
        return {
            restrict: 'A',
            scope: false,
            link: function (scope, el, attrs) {

            }
        };
    }]);
})(angular);