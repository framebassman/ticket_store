export async function nextFrame(): Promise<void> {
    return new Promise((res, rej) => {
        setTimeout(
            () => { res(); },
            0
        );
    });
}
