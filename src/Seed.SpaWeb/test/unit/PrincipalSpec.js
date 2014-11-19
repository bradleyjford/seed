describe('seedApp.security.Principal', function () {
    'use strict';

    var principal;

    beforeEach(module('seedApp.security'));

    beforeEach(inject(function () {
        principal = new seedApp.security.Principal();
    }));

    afterEach(function () {
    });

    it("isAuthenticated is set to false by default", function () {
        expect(principal.isAuthenticated).toBe(false);
    })

    it("signIn sets isAuthenticated to true", function () {
        principal.signIn('Fred', ['admin', 'dev']);

        expect(principal.name).toBe("Fred");
    });

    it("returns true when the principal is a member of a role", function () {
        principal.signIn('Fred', ['admin', 'dev']);

        expect(principal.isInRole('dev')).toBe(true);
    });

    it("returns false when the principal is not a member of a role", function () {
        principal.signIn('Fred', ['admin', 'dev']);

        expect(principal.isInRole('test')).toBe(false);
    });
});