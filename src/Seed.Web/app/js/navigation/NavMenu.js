(function (angular) {
    'use strict';

    angular.module('seedApp.navigation')
        .directive('navMenu', function ($location) {
            return {
                restrict: 'E',
                scope: false,
                link: function (scope, element, attrs) {
                    var links = element.find('a'),
                        onClass = attrs.navMenu || 'active',
                        routePattern,
                        link,
                        url,
                        currentLink,
                        urlMap = {};

                    if (!$location.$$html5) {
                        routePattern = /^#[^/]*/;
                    }

                    for (var i = 0; i < links.length; i++) {
                        link = angular.element(links[i]);
                        url = link.attr('href');

                        if ($location.$$html5) {
                            urlMap[url] = link;
                        } else {
                            urlMap[url.replace(routePattern, '')] = link;
                        }
                    }

                    scope.$on('$routeChangeStart', function () {
                        var pathLink = urlMap[$location.path()];

                        if (pathLink) {
                            if (currentLink) {
                                currentLink.removeClass(onClass);
                            }
                            currentLink = pathLink;
                            currentLink.addClass(onClass);
                        }
                    });
                }
            };
        });
})(angular);
