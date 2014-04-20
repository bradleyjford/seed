(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.controller('ValidationController', ['$scope', function ($scope) {

    }]);

    module.directive('validationSummary', [function () {
        return {
            restrict: 'AE',
            templateUrl: 'common/ValidationSummary.html',
            scope: {
                errors: '@'
            },
            controller: 'ValidationController'
        };
    }]);
})(angular);