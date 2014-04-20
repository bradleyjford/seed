(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.controller('SpinnerController', ['$scope', function ($scope) {
        $scope.spinner = {
            isBusy: false,
            pendingRequests: 0
        };

        $scope.requestStarted = function () {
            $scope.spinner.pendingRequests++;

            $scope.spinner.isBusy = true;
        };

        $scope.requestCompleted = function () {
            $scope.spinner.pendingRequests--;

            if ($scope.spinner.pendingRequests <= 0) {
                $scope.spinner.pendingRequests = 0;

                $scope.spinner.isBusy = false;
            }
        };

        $scope.$on('httpRequest', function (event, config) {
            $scope.requestStarted();
        });

        $scope.$on('httpRequestError', function (event, rejection) {
            $scope.requestCompleted();
        });

        $scope.$on('httpResponse', function (event, response) {
            $scope.requestCompleted();
        });

        $scope.$on('httpResponseError', function (event, rejection) {
            $scope.requestCompleted();
        });
    }]);

    module.directive('spinner', [function () {
        return {
            restrict: 'EA',
            templateUrl: 'common/Spinner.html',
            scope: { },
            controller: 'SpinnerController'
        };
    }]);
})(angular);
