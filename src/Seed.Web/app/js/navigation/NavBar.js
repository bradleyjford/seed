(function (angular) {
    'use strict';

    angular.module('seedApp.navigation')
        .directive('navBar', [function () {
            return {
                restrict: 'AE',
                templateUrl: 'navigation/NavBar.html'
            };
        }]);
})(angular);


