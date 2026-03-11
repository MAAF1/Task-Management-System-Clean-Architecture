export interface RegisterResponseDto {
    succeeded: boolean;
    message: string;
    errors?: string[];

    /* public bool Succeeded { get; set; }
 public string Message { get; set; } = null!;
 public IEnumerable<string>? Errors { get; set; }*/
}
