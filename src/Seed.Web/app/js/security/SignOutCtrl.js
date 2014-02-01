(function (angular) {
    'use strict';

    angular.module('seedApp.security')
        .controller('SignOutCtrl', ['$location', 'AuthenticationSvc', function ($location, AuthenticationSvc) {
            AuthenticationSvc.signOut()
                .success($location.path('/signin'));
        }]);

    angular.module('seedApp.security')
        .controller('TestCtrl', ['$scope', 'AuthenticationSvc', function ($scope, AuthenticationSvc) {
            AuthenticationSvc.test();
        }]);
})(angular);