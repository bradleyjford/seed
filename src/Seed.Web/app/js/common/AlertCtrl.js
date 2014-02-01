(function (angular) {
    'use strict';

    angular.module('seedApp.common', [])
        .controller('AlertCtrl', ['$scope', function ($scope) {
            $scope.alerts = [];
        }]);
})(angular);

