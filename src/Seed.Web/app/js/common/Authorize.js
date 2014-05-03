(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saAuthorize', ['SecurityPrincipal', function (SecurityPrincipal) {
        return function (scope, element, attrs) {
            var originalDisplay = element.css('display');

            scope.$on('securityPrincipalSignedIn', function (event) {
                if (attrs.saAuthorize === '' ||
                    SecurityPrincipal.isInRole(attrs.saAuthorize)) {

                    element.css('display', originalDisplay);
                }
                else {
                    element.css('display', 'none');
                }
            });

            scope.$on('securityPrincipalSignedOut', function (event) {
                element.css('display', 'none');
            });
        };
    }]);
})(angular);