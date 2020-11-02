import { browser, by, element } from 'protractor';

export class AppPage {
  navigateToBaseUrl(): Promise<unknown> {
    return browser.get(browser.baseUrl) as Promise<unknown>;
  }

  getLoginLegendText(): Promise<string> {
    return element(by.xpath('/html/body/app-root/app-login/div/mat-card/form/fieldset/legend')).getText() as Promise<string>;
  }
}
