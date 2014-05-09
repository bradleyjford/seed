(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignOutController', ['SecurityPrincipal', '$state',
        function (SecurityPrincipal, $state) {
            SecurityPrincipal.signOut()
                .then(function () {
                    $state.go('sign-in');
                });
        }
    ]);
})(angular);