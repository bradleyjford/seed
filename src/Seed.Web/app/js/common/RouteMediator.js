(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.factory('RouteMediator', ['$state', '$rootScope', '$interpolate', '$log', 'SecurityPrincipal',
        function ($state, $rootScope, $interpolate, $log, SecurityPrincipal) {
            function setRoutingHandlers() {
                $rootScope.page = {
                    title: ''
                };

                // ensure the current user is in the correct role for the requested route
                $rootScope.$on('$stateChangeStart', function (event, toState) {
                    if (toState.data.authorize && !SecurityPrincipal.isInRole(toState.data.authorize)) {
                        event.preventDefault();
/*
                        if (!SecurityPrincipal.isAuthenticated) {
                            $state.go('sign-in', { returnUrl: toState.url });
                            return;
                        }
*/
                        $state.go('403');
                    }
                });

                $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                    $rootScope.page.title = $interpolate(toState.data.title)($state.$current.locals.globals);
                });

                $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
                    $log.error(error);
                });
            }

            var service = {
                setRoutingHandlers: setRoutingHandlers
            };

            return service;
        }]);
})(angular);
