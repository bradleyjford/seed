(function (angular) {
    'use strict';

    angular.module('seedApp.security')
        .controller('SignOutCtrl', ['$scope', '$state', 'AuthenticationApi', function ($scope, $state, AuthenticationApi) {
            AuthenticationApi.signOut()
                .success(function () {
                    $scope.user.signOut();

                    $state.go('sign-in');
                });
        }]);
})(angular);