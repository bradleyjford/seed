(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignOutController',
        ['SecurityPrincipal', '$state', 'AuthenticationApi',
        function (SecurityPrincipal, $state, AuthenticationApi) {
            AuthenticationApi.signOut()
                .success(function () {
                    SecurityPrincipal.clear();

                    $state.go('sign-in');
                });
        }]);
})(angular);