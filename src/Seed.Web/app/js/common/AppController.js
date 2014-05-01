(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('AppController', ['$rootScope', '$state', 'SecurityPrincipal',
            function ($rootScope, $state, SecurityPrincipal) {

                // ensure the current user is in the correct role for the requested route
                $rootScope.$on('$stateChangeStart', function (event, toState) {
                    if (toState.data.requireRole && !SecurityPrincipal.isInRole(toState.data.requireRole)) {
                        event.preventDefault();

                        if (!SecurityPrincipal.isAuthenticated) {
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