(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saSpinner', [function () {
        return {
            restrict: 'EA',
            templateUrl: 'ui/Spinner.html',
            scope: { },
            controller: ['$scope', function ($scope) {
                $scope.enabled = false;

                var startCount = 0;

                function start() {
                    startCount++;

                    $scope.enabled = true;
                }

                function stop(force) {
                    startCount--;

                    if (force || startCount < 0) {
                        startCount = 0;
                    }

                    if (startCount === 0)
                    {
                        $scope.enabled = false;
                    }
                }

                $scope.$on('httpRequest', function (event, config) {
                    start();
                });

                $scope.$on('httpRequestError', function (event, rejection) {
                    stop();
                });

                $scope.$on('httpResponse', function (event, response) {
                    stop();
                });

                $scope.$on('httpResponseError', function (event, rejection) {
                    stop();
                });
            }]
        };
    }]);
})(angular);
