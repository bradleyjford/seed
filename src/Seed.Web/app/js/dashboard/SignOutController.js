(function (angular) {
    'use strict';

    var module = angular.module('seedApp.dashboard');

    module.controller('SignOutController',
        ['Principal', '$state', 'AuthenticationApi',
        function (Principal, $state, AuthenticationApi) {
            AuthenticationApi.signOut()
                .success(function () {
                    Principal.signOut();

                    $state.go('sign-in');
                });
        }]);
})(angular);