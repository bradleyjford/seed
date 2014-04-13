(function (angular) {
    'use strict';

    angular.module('seedApp')
        .directive('sAlerts', ['$rootScope', function ($rootScope) {
            return {
                restrict: 'E',
                templateUrl: 'common/Alert.html',
                scope: { },
                controller: function ($scope) {
                    $scope.alerts = [];

                    $rootScope.$on('seedApp.alertRaised', function (event, args) {
                        $scope.alerts.push(args);
                    });

                    $scope.closeAlert = function(index) {
                        console.log('here');
                        $scope.alerts.splice(index, 1);
                    };
                }
            };
        }]);
})(angular);