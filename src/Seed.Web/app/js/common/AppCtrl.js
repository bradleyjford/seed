(function (angular) {
    'use strict';

    angular.module('seedApp')
        .controller('AppCtrl', ['$rootScope', '$state', 'AuthenticationApi',
            function ($rootScope, $state, AuthenticationApi) {
                if (!$rootScope.page) {
                    $rootScope.page = new seedApp.Page();
                }

                // if the page has been reloaded from the server, obtain the signed in user
                // principal for rebinding the session
                if (!$rootScope.user) {
                    $rootScope.user = new seedApp.security.Principal();

                    AuthenticationApi.get()
                        .success(function (data) {
                            $rootScope.user.signIn(data.userName, data.roles);
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