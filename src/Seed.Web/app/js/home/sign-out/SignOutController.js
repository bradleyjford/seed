(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignOutController',
        ['SecurityPrincipal', '$state', 'AuthenticationApi',
        function (SecurityPrincipal, $state) {
            SecurityPrincipal.signOut()
                .success(function () {
                    $state.go('sign-in');
                });
        }]);
})(angular);