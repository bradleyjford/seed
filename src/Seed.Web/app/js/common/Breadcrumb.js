(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saBreadcrumb', ['$state', function ($state) {
        return {
            restrict: 'EA',
            templateUrl: 'common/Breadcrumb.html',
            scope: {
                itemDisplayNameResolver: '&'
            },
            controller: ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {

                var defaultResolver = function (state) {
                    return state.data.title || state.name;
                };

                var isCurrent = function(state){
                    return $state.$current.name === state.name;
                };

                var setNavigationState = function () {
                    $scope.navigationState = {
                        currentState: $state.$current,
                        params: $stateParams,
                        getDisplayName: function (state) {

                            if ($scope.hasCustomResolver) {
                                return $scope.itemDisplayNameResolver({
                                    defaultResolver: defaultResolver,
                                    state: state,
                                    isCurrent: isCurrent(state)
                                });
                            }

                            return defaultResolver(state);
                        },
                        isCurrent: isCurrent
                    };
                };

                $scope.$on('$stateChangeSuccess', function () {
                    setNavigationState();
                });

                setNavigationState();
            }],
            link: function (scope, element, attrs, controller) {
                scope.hasCustomResolver = angular.isDefined(attrs['itemDisplayNameResolver']);
            }
        };
    }]);
})(angular);
