(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('SpinnerController', ['$scope', function ($scope) {
            $scope.spinner = {
                isBusy: false
            };

            $scope.$on('httpRequest', function (event, config) {
                $scope.spinner.isBusy = true;
            });

            $scope.$on('httpRequestError', function (event, rejection) {
                $scope.spinner.isBusy = false;
            });

            $scope.$on('httpResponse', function (event, response) {
                $scope.spinner.isBusy = false;
            });

            $scope.$on('httpResponseError', function (event, rejection) {
                $scope.spinner.isBusy = false;
            });
        }]);

    angular.module('seedApp')
        .directive('spinner', [function () {
            return {
                restrict: 'EA',
                templateUrl: 'common/Spinner.html',
                scope: { },
                controller: 'SpinnerController'
            };
        }]);
})(angular);
