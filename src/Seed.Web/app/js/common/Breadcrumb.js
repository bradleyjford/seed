(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    /*
    Inspired by https://github.com/michaelbromley/angularUtils/tree/master/src/directives/uiBreadcrumbs
     */

    module.directive('saBreadcrumb', ['$interpolate', '$state', function ($interpolate, $state) {
        return {
            restrict: 'EA',
            templateUrl: 'common/Breadcrumb.html',
            scope: {
                displayNameProperty: '@'
            },
            link: function (scope) {
                /**
                 * Start with the current state and traverse up the path to build the
                 * array of breadcrumbs that can be used in an ng-repeat in the template.
                 */
                function updateBreadcrumbArray() {
                    var breadcrumbs = [];
                    var currentState = $state.$current;

                    while (currentState && currentState.name !== '') {
                        var displayName = getDisplayName(currentState);

                        if (displayName !== false && !currentState.abstract) {
                            breadcrumbs.push({
                                displayName: displayName,
                                route: currentState.name
                            });
                        }

                        currentState = currentState.parent;
                    }

                    breadcrumbs.reverse();

                    scope.breadcrumbs = breadcrumbs;
                }

                /**
                 * Resolve the displayName of the specified state. Take the property specified by the `displayname-property`
                 * attribute and look up the corresponding property on the state's config object. The specified string can be interpolated against any resolved
                 * properties on the state config object, by using the usual {{ }} syntax.
                 * @param currentState
                 * @returns {*}
                 */
                function getDisplayName(currentState) {
                    if (!scope.displayNameProperty) {
                        // if the display-name-property attribute was not specified, default to the state's name
                        return currentState.name;
                    }

                    var propertyArray = scope.displayNameProperty.split('.');
                    var propertyReference = currentState;

                    for (var i = 0; i < propertyArray.length; i++) {
                        if (angular.isDefined(propertyReference[propertyArray[i]])) {
                            if (propertyReference[propertyArray[i]] === false) {
                                return false;
                            }
                            else {
                                propertyReference = propertyReference[propertyArray[i]];
                            }
                        }
                        else {
                            // if the specified property was not found, default to the state's name
                            return currentState.name;
                        }
                    }

                    // use the $interpolate service to handle any bindings in the propertyReference string.
                    return $interpolate(propertyReference)(currentState.locals.globals);
                }

                scope.breadcrumbs = [];

                if ($state.$current.name !== '') {
                    updateBreadcrumbArray();
                }

                scope.$on('$stateChangeSuccess', function () {
                    updateBreadcrumbArray();
                });
            }
        };
    }]);
})(angular);
