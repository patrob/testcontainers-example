import { AppComponent } from './app.component';
import {renderRootComponent} from "../testing/testing-render";
import {TestingComponentRenderResult} from "../testing/testing-render-result";

describe('AppComponent', () => {
  let fixture: TestingComponentRenderResult<AppComponent>;

  beforeEach(async () => {
    fixture = await renderRootComponent(AppComponent);
  });

  it('should create the app', () => {
    const app = fixture.getComponentRef();
    expect(app).toBeTruthy();
  });

  it(`should have the 'testcontainers-example' title`, () => {
    const app = fixture.getComponentRef();
    expect(app.title).toEqual('testcontainers-example');
  });

  it('should render title', () => {
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, testcontainers-example');
  });
});
