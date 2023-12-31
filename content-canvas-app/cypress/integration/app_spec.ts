describe('My App', () => {
    it('loads successfully', () => {
      cy.visit('http://localhost:3000'); // Change to your app's URL
      cy.contains('My Component').click();
      // More actions and assertions
    });
  });
  