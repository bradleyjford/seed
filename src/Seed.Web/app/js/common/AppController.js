(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.controller('AppController', ['$rootScope', '$state', 'user',
        function ($rootScope, $state, user) {

            // ensure the current user is in the correct role for the requested route
            $rootScope.$on('$stateChangeStart', function (event, toState) {
                if (toState.data.requireRole && !user.isInRole(toState.data.requireRole)) {
                    event.preventDefault();

                    if (!user.isAuthenticated) {
                        $state.go('sign-in');
                        return;
                    }

                    $state.go('403');
                }
            });

            $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
                console.log(error);
            });
        }
    ]);
})(angular);