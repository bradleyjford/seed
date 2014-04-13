(function (angular) {
    'use strict';

    angular.module('seedApp.security')
        .controller('SignOutCtrl', ['$scope', '$location', 'AuthenticationApi', function ($scope, $location, AuthenticationApi) {
            AuthenticationApi.signOut()
                .success(function () {
                    $scope.user.signOut();

                    $location.path('/signin');
                });
        }]);
})(angular);