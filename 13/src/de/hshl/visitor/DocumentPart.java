package de.hshl.visitor;

public abstract class DocumentPart {
	protected String text;

	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}
	
	public DocumentPart(String text) {
		setText(text);
	}
	
	public abstract void accept(DocumentConverterVisitor visitor);
}