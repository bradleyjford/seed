(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('AppController', ['$interpolate', '$rootScope', '$state', 'AuthenticationApi', 'SecurityPrincipal',
            function ($interpolate, $rootScope, $state, AuthenticationApi, SecurityPrincipal) {
                $rootScope.page = {
                    title: ''
                };

                // if the page has been reloaded from the server, obtain the signed in user
                // principal for rebinding the session
                $rootScope.user = SecurityPrincipal;

                AuthenticationApi.get()
                    .success(function (data) {
                        SecurityPrincipal.set(data.username, data.fullName, data.roles);

                        if ($state.current.name === '/') {
                            $state.go('home');
                        }
                    });

                // ensure the current user is in the correct role for the requested route
                $rootScope.$on('$stateChangeStart', function (event, toState) {
                    if (toState.data.requireRole && !$rootScope.user.isInRole(toState.data.requireRole)) {
                        if (!$rootScope.user.isAuthenticated) {
                            $state.go('sign-in');
                            return;
                        }

                        $state.go('403');
                    }
                });

                $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                    $rootScope.page.title = $interpolate(toState.data.title)($state.$current.locals.globals);
                });
            }
        ]);
})(angular);