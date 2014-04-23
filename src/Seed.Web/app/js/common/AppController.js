(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('AppController', ['$rootScope', '$state', 'AuthenticationApi', 'Principal',
            function ($rootScope, $state, AuthenticationApi, Principal) {
                if (!$rootScope.page) {
                    $rootScope.page = new seedApp.Page();
                }

                // if the page has been reloaded from the server, obtain the signed in user
                // principal for rebinding the session
                if (!$rootScope.user) {
                    $rootScope.user = Principal;

                    AuthenticationApi.get()
                        .success(function (data) {
                            Principal.signIn(data.userName, data.roles);

                            if ($state.current.name === 'sign-in') {
                                $state.go('dashboard.home');
                            }
                        });
                }

                // ensure the current user is in the correct role for the requested route
/*                $rootScope.$on('$routeChangeStart', function (event, next) {
                    if (next.requireRole && !$rootScope.user.isInRole(next.requireRole)) {
                        if (!$rootScope.user.isAuthenticated) {
                            $state.go('sign-in');
                            return;
                        }

                        $state.go('403');
                    }
                });*/

                $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                    $rootScope.page.title = toState.data.title;
                });
            }
        ]);
})(angular);