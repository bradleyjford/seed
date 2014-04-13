(function (angular) {
    'use strict';

    angular.module('seedApp')
        .service('AlertSvc', [function () {
            var Alert = seedApp.Alert;

            var eventName = 'seedApp.alertRaised';

            var success = function ($scope, message) {
                $scope.$emit(eventName, new Alert('success', message));
            };

            var info = function ($scope, message) {
                $scope.$emit(eventName, new Alert('info', message));
            };

            var warning = function ($scope, message) {
                $scope.$emit(eventName, new Alert('warning', message));
            };

            var danger = function ($scope, message) {
                $scope.$emit(eventName, new Alert('danger', message));
            };

            return {
                eventName: eventName,
                success: success,
                info: info,
                warning: warning,
                danger: danger
            };
        }]);
})(angular);