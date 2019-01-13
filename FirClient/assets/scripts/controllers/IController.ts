
interface IController {
    initialize(): void;
    onViewCreated(go): void;
    update(dt): void;
    getResPath(): string;
    dispose(): void;
}
