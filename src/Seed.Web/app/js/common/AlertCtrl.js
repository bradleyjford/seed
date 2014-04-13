(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('AlertCtrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
            $scope.alerts = [];

            $rootScope.$on('seedApp.alertRaised', function (event, args) {
                $scope.alerts.splice(0, 0, args);
            });

            $scope.closeAlert = function(index) {
                $scope.alerts.splice(index, 1);
            };
        }]);
})(angular);